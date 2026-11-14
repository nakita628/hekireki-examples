defmodule LangWatch.DataPrivacyPolicy do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          scope_type: atom(),
          scope_id: String.t(),
          personal_only: boolean(),
          config: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "DataPrivacyPolicy" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:scope_type, Ecto.Enum, values: [:ORGANIZATION, :DEPARTMENT, :TEAM, :PROJECT], source: :scopeType)
    field(:scope_id, :string, source: :scopeId)
    field(:personal_only, :boolean, default: false, source: :personalOnly)
    field(:config, :map)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end