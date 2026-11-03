defmodule Postiz.UserOrganization do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          disabled: boolean(),
          role: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Postiz.Organization.t() | nil,
          user: Postiz.User.t() | nil
        }

  schema "UserOrganization" do
    field(:disabled, :boolean, default: false)
    field(:role, Ecto.Enum, values: [:SUPERADMIN, :ADMIN, :USER], default: :USER)
    field(:organization_id, :binary_id, source: :organizationId)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:organization, Postiz.Organization, foreign_key: :organization_id, define_field: false)
    belongs_to(:user, Postiz.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end