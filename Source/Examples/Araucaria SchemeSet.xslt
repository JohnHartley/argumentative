<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<!-- Reads an Araucaria SchemeSet -->
<xsl:output method="text"/>
  <xsl:template match="/">
<xsl:apply-templates select="ARG/SCHEMESET"/>
</xsl:template>
  
<xsl:template match="SCHEMESET">
<xsl:text>Schemeset</xsl:text>
<xsl:call-template name="newline"/>
<xsl:apply-templates select="SCHEME"/>
</xsl:template>

<xsl:template match="SCHEME">
<xsl:text>Scheme: </xsl:text>
<xsl:value-of select="NAME"/>
<xsl:call-template name="newline"/>
<xsl:apply-templates select="FORM"/>
<xsl:call-template name="newline"/>
<xsl:text>There are </xsl:text>
<xsl:value-of select="count(./CQ)"/>
<xsl:text> critical questions.</xsl:text>
<xsl:call-template name="newline"/>
<xsl:apply-templates select="CQ"/>
<xsl:call-template name="newline"/>
</xsl:template>

<xsl:template match="FORM/PREMISE">
<xsl:text>Premise: </xsl:text>
<xsl:value-of select="."/>
<xsl:call-template name="newline"/>
</xsl:template>

<xsl:template match="FORM/CONCLUSION">
<xsl:text>Conclusion: </xsl:text>
<xsl:value-of select="."/>
<xsl:call-template name="newline"/>
</xsl:template>

<xsl:template match="CQ">
<xsl:text>Critical Question: </xsl:text>
<xsl:value-of select="."/>
<xsl:call-template name="newline"/>
</xsl:template>

<xsl:template name="newline">
<xsl:text>
</xsl:text>
</xsl:template>

</xsl:stylesheet>
