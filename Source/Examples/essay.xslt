<?xml version="1.0" encoding="UTF-8" ?>
<!-- This stylesheet produces a very ordinary essay from a standard AXL argument -->
<!-- Version 0.2 November 2006 -->
<xsl:stylesheet version = '1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
<xsl:output method="text"/>

<xsl:template match="/">
<!-- will just select the starting premise of which there will only be one -->
<xsl:apply-templates select="argument"/>
</xsl:template>

<xsl:template match="premise">
	<xsl:text>The main premise for the argument presentented in this essay is that </xsl:text>
	<xsl:apply-templates select="title"/>
<xsl:text>.
</xsl:text>
	<xsl:apply-templates select="reason|objection|helper"/>
	<xsl:text>In conclusion</xsl:text>
	<xsl:choose>
  	<xsl:when test="count(//premise/objection)=1">
  	  <xsl:apply-templates select="//premise/objection/title"/>
  	  <xsl:text>,</xsl:text>
	  </xsl:when>
	  <xsl:when test="count(//premise/objection)>1">
  	  <xsl:text>, although there are the objections: </xsl:text>
  	  <xsl:apply-templates mode="objections"/>
  	  <xsl:text>, </xsl:text>
	  </xsl:when>
	</xsl:choose>
	
	<xsl:choose>
  	<xsl:when test="count(//premise/reason)=1">
  	  <xsl:text>, the main premise is supported by  the reason, </xsl:text>
  	  <xsl:apply-templates select="//premise/reason/title"/>
  	  <xsl:text>.</xsl:text>
	  </xsl:when>
	  <xsl:when test="count(//premise/reason)>1">
  	  <xsl:text>, the main premise is supported by the reasons: </xsl:text>
  	  <xsl:apply-templates mode="reasons"/><xsl:text>.</xsl:text>
	  </xsl:when>
	</xsl:choose>
</xsl:template>

	<!-- Make sure title begins with a lower case letter and ends with a full stop -->
<xsl:template match="title">
  <xsl:value-of select="translate(substring(.,1,1),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')"/>
  <xsl:choose>
    <xsl:when test=" substring(.,string-length(.))='.' "> <!-- If there is a full stop, trim it -->
      <xsl:value-of select="substring(.,2,string-length(.)-1)"/>
    </xsl:when>
    <xsl:otherwise>
      <xsl:value-of select="substring(.,2)"/>
    </xsl:otherwise>
</xsl:choose>
</xsl:template>	

<xsl:template match="premise/objection" mode="objections">
<xsl:value-of select="title"/><xsl:text>, </xsl:text>
<xsl:apply-templates select="Title"/>
</xsl:template>

<xsl:template match="text()" mode="objections"/>

<xsl:template match="premise/reason" mode="reasons">
<xsl:value-of select="title"/><xsl:text>, </xsl:text>
</xsl:template>

<xsl:template match="text()" mode="reasons"/>

<xsl:template match="premise/reason">
	<xsl:text>A main reason for the premise is </xsl:text>
	<xsl:apply-templates select="title"/>
<xsl:text>.
</xsl:text>
	<xsl:apply-templates select="reason|objection|helper"/>
</xsl:template>

<xsl:template match="reason">
	<xsl:text>A reason supporting the assertion </xsl:text>
	<xsl:apply-templates select="../title"/>
	<xsl:text> is that </xsl:text>
	<xsl:apply-templates select="title"/>
<xsl:text>.
</xsl:text>
	<xsl:apply-templates select="reason|objection|helper"/>
</xsl:template>

<xsl:template match="premise/objection">
	<xsl:text>A main objection for the premise is </xsl:text>
	<xsl:apply-templates select="title"/>
<xsl:text>
</xsl:text>
	<xsl:apply-templates select="reason|objection|helper"/>
</xsl:template>

<xsl:template match="objection">
	<xsl:text>A objection against the assertion </xsl:text>
	<xsl:apply-templates select="../title"/>
	<xsl:text> is that </xsl:text>
	<xsl:value-of select="title"/>
<xsl:text>.
</xsl:text>
	<xsl:apply-templates select="reason|objection|helper"/>
</xsl:template>

<xsl:template match="helper">
	<xsl:text>Futhermore </xsl:text>
	<xsl:apply-templates select="title"/>
<xsl:text>.
</xsl:text>
	<xsl:apply-templates select="reason|objection|helper"/>
</xsl:template>

<xsl:template match="text()"/>
</xsl:stylesheet>