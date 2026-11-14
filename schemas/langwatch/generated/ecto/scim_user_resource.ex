defmodule LangWatch.ScimUserResource do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: String.t(),
          organization_id: String.t(),
          user_name: String.t(),
          name: String.t() | nil,
          active: boolean(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: LangWatch.User.t() | nil
        }

  schema "ScimUserResource" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:user_name, :string, source: :userName)
    field(:name, :string)
    field(:active, :boolean, default: true)
    field(:deleted_at, LangWatch.PrismaDateTime, source: :deletedAt)
    field(:user_id, :string, primary_key: true, source: :userId)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end