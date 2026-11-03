class MarketData < ApplicationRecord
  self.table_name = "MarketData"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :isCarriedForward, default: false
  alias_attribute :created_at, :createdAt

  enum :dataSource, { alpha_vantage: "ALPHA_VANTAGE", coingecko: "COINGECKO", eod_historical_data: "EOD_HISTORICAL_DATA", financial_modeling_prep: "FINANCIAL_MODELING_PREP", ghostfolio: "GHOSTFOLIO", google_sheets: "GOOGLE_SHEETS", manual: "MANUAL", rapid_api: "RAPID_API", yahoo: "YAHOO" },
       validate: true
  enum :state, { close: "CLOSE", intraday: "INTRADAY" }, default: :close, validate: true

  validates :date, presence: true
  validates :marketPrice, presence: true
  validates :symbol, presence: true, uniqueness: { scope: [ :dataSource, :date ] }
end
