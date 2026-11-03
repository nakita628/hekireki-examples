defmodule Ghostfolio.AssetProfileSplit do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          date: DateTime.t(),
          denominator: integer(),
          numerator: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          symbol_profile: Ghostfolio.SymbolProfile.t() | nil
        }

  schema "AssetProfileSplit" do
    field(:date, Ghostfolio.PrismaDateTime)
    field(:denominator, :integer)
    field(:numerator, :integer)
    field(:symbol_profile_id, :binary_id, source: :symbolProfileId)
    belongs_to(:symbol_profile, Ghostfolio.SymbolProfile, foreign_key: :symbol_profile_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end