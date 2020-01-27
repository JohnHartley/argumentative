<?xml version="1.0" encoding="UTF-8"?>
<!-- Exports an Argument (*.axl) file to plain text including comments if present-->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="text"/>
  <xsl:variable name="newline">
    <xsl:text>&#xD;&#xA;</xsl:text>
  </xsl:variable>
  <xsl:template match="/">
    <xsl:apply-templates select="argument"/>
  </xsl:template>
  <xsl:template match="premise">
    <xsl:text>Premise:</xsl:text>
    <xsl:value-of select="title"/>
    <xsl:value-of select="$newline"/>
    <xsl:apply-templates select="reason|objection|helper|comment"/>
  </xsl:template>
  <xsl:template match="reason">
    <xsl:number level="multiple" count="reason|objection|helper"/>
    <xsl:text> Reason:</xsl:text>
    <xsl:value-of select="title"/>
    <xsl:value-of select="$newline"/>
    <xsl:apply-templates select="reason|objection|helper|comment"/>
  </xsl:template>
  <xsl:template match="objection">
    <xsl:number level="multiple" count="reason|objection|helper"/>
    <xsl:text> Objection:</xsl:text>
    <xsl:value-of select="title"/>
    <xsl:value-of select="$newline"/>
    <xsl:apply-templates select="reason|objection|helper|comment"/>
  </xsl:template>
  <xsl:template match="helper">
    <xsl:number level="multiple" count="reason|objection|helper"/>
    <xsl:text> helper:</xsl:text>
    <xsl:value-of select="title"/>
    <xsl:value-of select="$newline"/>
    <xsl:apply-templates select="reason|objection|helper|comment"/>
  </xsl:template>
  <xsl:template match="comment">
    <xsl:text>Comment: </xsl:text>
    <xsl:value-of select="."/>
    <xsl:value-of select="$newline"/>
  </xsl:template>
  <xsl:template match="text()"/>
</xsl:stylesheet>
