defmodule LangWatch.ScimExternalId do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          organization_id: String.t(),
          id: String.t(),
          connection_id: String.t(),
          external_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: LangWatch.User.t() | nil
        }

  schema "ScimExternalId" do
    field(:organization_id, :string, source: :organizationId)
    field(:id, :string, primary_key: true)
    field(:connection_id, :string, source: :connectionId)
    field(:external_id, :string, source: :externalId)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end