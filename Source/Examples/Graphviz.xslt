<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" 
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:meta="axl:meta">
  <!-- Exports an Argument (*.axl) file to Graphvis DOT format -->
  <meta:metadata>
    <title>Graphviz</title>
      <description>
      Converts and argument map to a Graphviz DOT file.
      See http://www.graphviz.org/ for downloads and documentation
    </description>
    <fileFormat>dot</fileFormat>
    <!-- The parameter section is for future use and will allow parametric XSLT calls -->
    <parameters>
      <parameter name="CompanyName" format="xs:string"
       Description="Company name to be inserted in the footer"/>

      <parameter name="DateDue" format="xs:date"
       Description="Date Due"/>

      <parameter name="IncludePicture" format="xs:boolean"
       Description="Do you want to include a graphical representation?"/>
    </parameters>
  </meta:metadata>

  <xsl:output method="text"/>
  <xsl:variable name="cr">
    <xsl:text>
</xsl:text>
  </xsl:variable>

  <xsl:template match="/">
    <xsl:text>digraph G {</xsl:text>
    <xsl:apply-templates select="argument"/>
    <xsl:value-of select="$cr"/>
    <xsl:text>}</xsl:text>
    <xsl:text></xsl:text>
  </xsl:template>

  <xsl:template match="premise">
    <xsl:value-of select="$cr"/>
    <xsl:text>/* Premise */</xsl:text>
    <xsl:value-of select="$cr"/>
    <xsl:text>&quot;</xsl:text>
    <xsl:value-of select="concat(local-name(.),ref)"/>
    <xsl:text>&quot; [label=&quot;</xsl:text>
    <xsl:value-of select="title"/>
    <xsl:text>&quot;]</xsl:text>
    <xsl:value-of select="$cr"/>
    <xsl:apply-templates select="reason|objection|helper"/>
  </xsl:template>

  <xsl:template match="reason">
    <xsl:call-template name="aNode">
      <xsl:with-param name="type">reason</xsl:with-param>
    </xsl:call-template>
    <xsl:apply-templates select="reason|objection|helper"/>
  </xsl:template>

  <xsl:template name="aNode">
    <xsl:param name="type">unknown</xsl:param>
    <xsl:value-of select="$cr"/>
    <xsl:text>/* A </xsl:text>
    <xsl:value-of select="$type"/>
    <xsl:text> */</xsl:text>
    <xsl:value-of select="$cr"/>
    <xsl:text>&quot;</xsl:text>
    <xsl:value-of select="concat(local-name(.),ref)"/>
    <xsl:text>&quot; [shape=box,label=&quot;</xsl:text>
    <xsl:value-of select="title"/>
    <xsl:text>&quot;]</xsl:text>
    <xsl:value-of select="$cr"/>
    <xsl:text>/* Connect </xsl:text>
    <xsl:value-of select="$type"/>
    <xsl:text> with parent */</xsl:text>
    <xsl:value-of select="$cr"/>
    <xsl:text>&quot;</xsl:text>
    <xsl:value-of select="concat(local-name(../.),../ref)"/>
    <xsl:text>&quot; -> &quot;</xsl:text>
    <xsl:value-of select="concat(local-name(.),ref)"/>
    <xsl:text>&quot;</xsl:text>
  </xsl:template>

  <xsl:template match="objection">
    <xsl:call-template name="aNode">
      <xsl:with-param name="type">objection</xsl:with-param>
    </xsl:call-template>
    <xsl:apply-templates select="reason|objection|helper"/>
  </xsl:template>

  <xsl:template match="helper">
    <xsl:call-template name="aNode">
      <xsl:with-param name="type">helper</xsl:with-param>
    </xsl:call-template>
    <xsl:apply-templates select="reason|objection|helper"/>
  </xsl:template>

  <xsl:template match="text()"/>
</xsl:stylesheet>
