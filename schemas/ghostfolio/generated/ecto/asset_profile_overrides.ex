defmodule Ghostfolio.AssetProfileOverrides do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          asset_class: atom() | nil,
          asset_sub_class: atom() | nil,
          countries: map() | nil,
          holdings: map() | nil,
          name: String.t() | nil,
          sectors: map() | nil,
          url: String.t() | nil,
          updated_at: DateTime.t(),
          symbol_profile: Ghostfolio.SymbolProfile.t() | nil
        }

  schema "SymbolProfileOverrides" do
    field(:asset_class, Ecto.Enum, values: [:ALTERNATIVE_INVESTMENT, :COMMODITY, :EQUITY, :FIXED_INCOME, :LIQUIDITY, :REAL_ESTATE], source: :assetClass)
    field(:asset_sub_class, Ecto.Enum, values: [:BOND, :CASH, :COLLECTIBLE, :COMMODITY, :CRYPTOCURRENCY, :ETF, :LOAN, :MUTUALFUND, :PRECIOUS_METAL, :PRIVATE_EQUITY, :STOCK], source: :assetSubClass)
    field(:countries, :map)
    field(:holdings, :map)
    field(:name, :string)
    field(:sectors, :map)
    field(:url, :string)
    field(:symbol_profile_id, :binary_id, source: :symbolProfileId)
    belongs_to(:symbol_profile, Ghostfolio.SymbolProfile, foreign_key: :symbol_profile_id, define_field: false, type: :binary_id)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end