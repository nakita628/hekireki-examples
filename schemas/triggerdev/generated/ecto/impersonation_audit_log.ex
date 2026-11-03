defmodule Trigger.ImpersonationAuditLog do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          action: atom(),
          ip_address: String.t() | nil,
          inserted_at: DateTime.t(),
          admin: Trigger.User.t() | nil,
          target: Trigger.User.t() | nil
        }

  schema "ImpersonationAuditLog" do
    field(:id, :string, primary_key: true)
    field(:action, Ecto.Enum, values: [:START, :STOP])
    field(:ip_address, :string, source: :ipAddress)
    field(:admin_id, :string, source: :adminId)
    field(:target_id, :string, source: :targetId)
    belongs_to(:admin, Trigger.User, foreign_key: :admin_id, define_field: false, type: :string)
    belongs_to(:target, Trigger.User, foreign_key: :target_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end