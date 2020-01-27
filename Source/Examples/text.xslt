<?xml version="1.0" encoding="UTF-8" ?>
<!-- Exports an Argument (*.axl) file to plain text -->
<xsl:stylesheet version = '1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
	<xsl:output method="text"/>
	
	<xsl:template match="/">
		<xsl:apply-templates select="argument"/>
	</xsl:template>
	
	<xsl:template match="premise">
		<xsl:text>Premise:</xsl:text>
		<xsl:value-of select="title"/>
		<xsl:text>
</xsl:text>
		<xsl:apply-templates select="reason|objection|helper"/>
	</xsl:template>
	
	<xsl:template match="reason">
		<xsl:number level="multiple" count="reason|objection|helper" />
		<xsl:text> Reason:</xsl:text>
		<xsl:value-of select="title"/>
		<xsl:text>
</xsl:text>
		<xsl:apply-templates select="reason|objection|helper"/>
	</xsl:template>
	
	<xsl:template match="objection">
		<xsl:number level="multiple" count="reason|objection|helper" />
		<xsl:text> Objection:</xsl:text>
		<xsl:value-of select="title"/>
		<xsl:text>
</xsl:text>
		<xsl:apply-templates select="reason|objection|helper"/>
	</xsl:template>
	
	<xsl:template match="helper">
		<xsl:number level="multiple" count="reason|objection|helper" />
		<xsl:text> helper:</xsl:text>
		<xsl:value-of select="title"/>
		<xsl:text>
</xsl:text>
		<xsl:apply-templates select="reason|objection|helper"/>
	</xsl:template>
	
	<xsl:template match="text()"/>
</xsl:stylesheet>
