class AssetProfileResolution < ApplicationRecord
  self.table_name = "AssetProfileResolution"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :requestCount, default: 1
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :dataSourceOrigin, { alpha_vantage: "ALPHA_VANTAGE", coingecko: "COINGECKO", eod_historical_data: "EOD_HISTORICAL_DATA", financial_modeling_prep: "FINANCIAL_MODELING_PREP", ghostfolio: "GHOSTFOLIO", google_sheets: "GOOGLE_SHEETS", manual: "MANUAL", rapid_api: "RAPID_API", yahoo: "YAHOO" },
       validate: true
  enum :dataSourceTarget, { alpha_vantage: "ALPHA_VANTAGE", coingecko: "COINGECKO", eod_historical_data: "EOD_HISTORICAL_DATA", financial_modeling_prep: "FINANCIAL_MODELING_PREP", ghostfolio: "GHOSTFOLIO", google_sheets: "GOOGLE_SHEETS", manual: "MANUAL", rapid_api: "RAPID_API", yahoo: "YAHOO" },
       prefix: true, validate: true

  validates :currency, presence: true
  validates :symbolOrigin, presence: true, uniqueness: { scope: :dataSourceOrigin }
  validates :symbolTarget, presence: true
end
