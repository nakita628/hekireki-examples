defmodule LangWatch.ModelDefaultConfig do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          config: map(),
          author_id: String.t() | nil,
          organization_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          scopes: [LangWatch.ModelDefaultConfigScope.t()]
        }

  schema "ModelDefaultConfig" do
    field(:id, :string, primary_key: true)
    field(:config, :map)
    field(:author_id, :string, source: :authorId)
    field(:organization_id, :string, source: :organizationId)
    has_many(:scopes, LangWatch.ModelDefaultConfigScope, foreign_key: :config_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end