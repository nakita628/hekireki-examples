defmodule LangWatch.ModelProviderScope do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          scope_type: atom(),
          scope_id: String.t(),
          inserted_at: DateTime.t(),
          model_provider: LangWatch.ModelProvider.t() | nil
        }

  schema "ModelProviderScope" do
    field(:id, :string, primary_key: true)
    field(:scope_type, Ecto.Enum, values: [:ORGANIZATION, :TEAM, :PROJECT], source: :scopeType)
    field(:scope_id, :string, source: :scopeId)
    field(:model_provider_id, :string, source: :modelProviderId)
    belongs_to(:model_provider, LangWatch.ModelProvider, foreign_key: :model_provider_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end