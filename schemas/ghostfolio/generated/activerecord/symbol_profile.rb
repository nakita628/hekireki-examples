class SymbolProfile < ApplicationRecord
  self.table_name = "SymbolProfile"
  self.implicit_order_column = "createdAt"

  attribute :holdings, default: -> { JSON.parse("[]") }
  attribute :id, default: -> { SecureRandom.uuid }
  attribute :isActive, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :assetClass, { alternative_investment: "ALTERNATIVE_INVESTMENT", commodity: "COMMODITY", equity: "EQUITY", fixed_income: "FIXED_INCOME", liquidity: "LIQUIDITY", real_estate: "REAL_ESTATE" },
       validate: { allow_nil: true }
  enum :assetSubClass, { bond: "BOND", cash: "CASH", collectible: "COLLECTIBLE", commodity: "COMMODITY", cryptocurrency: "CRYPTOCURRENCY", etf: "ETF", loan: "LOAN", mutualfund: "MUTUALFUND", precious_metal: "PRECIOUS_METAL", private_equity: "PRIVATE_EQUITY", stock: "STOCK" },
       prefix: true, validate: { allow_nil: true }
  enum :dataGatheringFrequency, { daily: "DAILY", hourly: "HOURLY" }, default: :daily, validate: true
  enum :dataSource, { alpha_vantage: "ALPHA_VANTAGE", coingecko: "COINGECKO", eod_historical_data: "EOD_HISTORICAL_DATA", financial_modeling_prep: "FINANCIAL_MODELING_PREP", ghostfolio: "GHOSTFOLIO", google_sheets: "GOOGLE_SHEETS", manual: "MANUAL", rapid_api: "RAPID_API", yahoo: "YAHOO" },
       validate: true

  validates :currency, presence: true
  validates :symbol, presence: true, uniqueness: { scope: :dataSource }

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :symbol_profile
  has_one :asset_profile_overrides, foreign_key: "symbolProfileId", inverse_of: :symbol_profile, dependent: :destroy
  has_many :activities, class_name: "Order", foreign_key: "symbolProfileId", inverse_of: :symbol_profile,
                        dependent: :restrict_with_error
  has_many :asset_profile_splits, foreign_key: "symbolProfileId", inverse_of: :symbol_profile, dependent: :destroy
  has_and_belongs_to_many :watched_by, class_name: "User", join_table: "_UserWatchlist", foreign_key: "A",
                                       association_foreign_key: "B"
end
