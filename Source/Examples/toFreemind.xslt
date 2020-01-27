<?xml version="1.0" encoding="UTF-8" ?>
<!-- translates AXL to MM - Freemind mind msp -->
<xsl:stylesheet version = '1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
<xsl:output method="xml"/>

<xsl:template match="/">
	<map>
	<xsl:apply-templates select="argument"/>
	</map>
</xsl:template>

<xsl:template match="premise">
	<node>
	<xsl:attribute name="TEXT">
	<xsl:value-of select="title"/>
	</xsl:attribute>
	<xsl:apply-templates select="reason|objection"/>
	</node>
</xsl:template>

<xsl:template match="reason">
	<node>
	<xsl:attribute name="TEXT">
	<xsl:value-of select="title"/>
	</xsl:attribute>
	<xsl:apply-templates select="reason|objection|helper"/>
	</node>
</xsl:template>

<xsl:template match="objection">
	<node>
	<xsl:attribute name="TEXT">
	<xsl:value-of select="title"/>
	</xsl:attribute>
	<xsl:apply-templates select="reason|objection|helper"/>
	</node>
</xsl:template>

<xsl:template match="helper">
	<node>
	<xsl:attribute name="TEXT">
	<xsl:value-of select="title"/>
	</xsl:attribute>
	<xsl:apply-templates select="reason|objection|helper"/>
	</node>
</xsl:template>

<xsl:template match="text()"/>
</xsl:stylesheet>