defmodule Ghostfolio.SymbolProfile do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          asset_class: atom() | nil,
          asset_sub_class: atom() | nil,
          comment: String.t() | nil,
          countries: map() | nil,
          currency: String.t(),
          cusip: String.t() | nil,
          data_gathering_frequency: atom(),
          data_source: atom(),
          figi: String.t() | nil,
          figi_composite: String.t() | nil,
          figi_share_class: String.t() | nil,
          holdings: map() | nil,
          is_active: boolean(),
          isin: String.t() | nil,
          name: String.t() | nil,
          scraper_configuration: map() | nil,
          sectors: map() | nil,
          symbol: String.t(),
          symbol_mapping: map() | nil,
          url: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Ghostfolio.User.t() | nil,
          asset_profile_overrides: Ghostfolio.AssetProfileOverrides.t() | nil,
          activities: [Ghostfolio.Order.t()],
          asset_profile_splits: [Ghostfolio.AssetProfileSplit.t()],
          watched_by: [Ghostfolio.User.t()]
        }

  schema "SymbolProfile" do
    field(:asset_class, Ecto.Enum, values: [:ALTERNATIVE_INVESTMENT, :COMMODITY, :EQUITY, :FIXED_INCOME, :LIQUIDITY, :REAL_ESTATE], source: :assetClass)
    field(:asset_sub_class, Ecto.Enum, values: [:BOND, :CASH, :COLLECTIBLE, :COMMODITY, :CRYPTOCURRENCY, :ETF, :LOAN, :MUTUALFUND, :PRECIOUS_METAL, :PRIVATE_EQUITY, :STOCK], source: :assetSubClass)
    field(:comment, :string)
    field(:countries, :map)
    field(:currency, :string)
    field(:cusip, :string)
    field(:data_gathering_frequency, Ecto.Enum, values: [:DAILY, :HOURLY], default: :DAILY, source: :dataGatheringFrequency)
    field(:data_source, Ecto.Enum, values: [:ALPHA_VANTAGE, :COINGECKO, :EOD_HISTORICAL_DATA, :FINANCIAL_MODELING_PREP, :GHOSTFOLIO, :GOOGLE_SHEETS, :MANUAL, :RAPID_API, :YAHOO], source: :dataSource)
    field(:figi, :string)
    field(:figi_composite, :string, source: :figiComposite)
    field(:figi_share_class, :string, source: :figiShareClass)
    field(:holdings, :map)
    field(:is_active, :boolean, default: true, source: :isActive)
    field(:isin, :string)
    field(:name, :string)
    field(:scraper_configuration, :map, source: :scraperConfiguration)
    field(:sectors, :map)
    field(:symbol, :string)
    field(:symbol_mapping, :map, source: :symbolMapping)
    field(:url, :string)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:user, Ghostfolio.User, foreign_key: :user_id, define_field: false)
    has_one(:asset_profile_overrides, Ghostfolio.AssetProfileOverrides, foreign_key: :symbol_profile_id)
    has_many(:activities, Ghostfolio.Order, foreign_key: :symbol_profile_id)
    has_many(:asset_profile_splits, Ghostfolio.AssetProfileSplit, foreign_key: :symbol_profile_id)
    many_to_many(:watched_by, Ghostfolio.User, join_through: "_UserWatchlist", join_keys: [A: :id, B: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end