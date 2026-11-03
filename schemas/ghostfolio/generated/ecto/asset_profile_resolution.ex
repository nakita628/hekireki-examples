defmodule Ghostfolio.AssetProfileResolution do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          currency: String.t(),
          data_source_origin: atom(),
          data_source_target: atom(),
          request_count: integer(),
          symbol_origin: String.t(),
          symbol_target: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "AssetProfileResolution" do
    field(:currency, :string)
    field(:data_source_origin, Ecto.Enum, values: [:ALPHA_VANTAGE, :COINGECKO, :EOD_HISTORICAL_DATA, :FINANCIAL_MODELING_PREP, :GHOSTFOLIO, :GOOGLE_SHEETS, :MANUAL, :RAPID_API, :YAHOO], source: :dataSourceOrigin)
    field(:data_source_target, Ecto.Enum, values: [:ALPHA_VANTAGE, :COINGECKO, :EOD_HISTORICAL_DATA, :FINANCIAL_MODELING_PREP, :GHOSTFOLIO, :GOOGLE_SHEETS, :MANUAL, :RAPID_API, :YAHOO], source: :dataSourceTarget)
    field(:request_count, :integer, default: 1, source: :requestCount)
    field(:symbol_origin, :string, source: :symbolOrigin)
    field(:symbol_target, :string, source: :symbolTarget)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end