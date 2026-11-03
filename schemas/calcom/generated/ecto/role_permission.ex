defmodule Calcom.RolePermission do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          resource: String.t(),
          action: String.t(),
          inserted_at: DateTime.t(),
          role: Calcom.Role.t() | nil
        }

  schema "RolePermission" do
    field(:resource, :string)
    field(:action, :string)
    field(:role_id, :string, source: :roleId)
    belongs_to(:role, Calcom.Role, foreign_key: :role_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end