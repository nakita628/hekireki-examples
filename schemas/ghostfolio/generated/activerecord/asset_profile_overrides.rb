class AssetProfileOverrides < ApplicationRecord
  self.table_name = "SymbolProfileOverrides"
  self.primary_key = "symbolProfileId"

  attribute :countries, default: -> { JSON.parse("[]") }
  attribute :holdings, default: -> { JSON.parse("[]") }
  attribute :sectors, default: -> { JSON.parse("[]") }
  alias_attribute :updated_at, :updatedAt

  enum :assetClass, { alternative_investment: "ALTERNATIVE_INVESTMENT", commodity: "COMMODITY", equity: "EQUITY", fixed_income: "FIXED_INCOME", liquidity: "LIQUIDITY", real_estate: "REAL_ESTATE" },
       validate: { allow_nil: true }
  enum :assetSubClass, { bond: "BOND", cash: "CASH", collectible: "COLLECTIBLE", commodity: "COMMODITY", cryptocurrency: "CRYPTOCURRENCY", etf: "ETF", loan: "LOAN", mutualfund: "MUTUALFUND", precious_metal: "PRECIOUS_METAL", private_equity: "PRIVATE_EQUITY", stock: "STOCK" },
       prefix: true, validate: { allow_nil: true }

  belongs_to :symbol_profile, foreign_key: "symbolProfileId", inverse_of: :asset_profile_overrides
end
