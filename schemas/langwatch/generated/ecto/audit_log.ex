defmodule LangWatch.AuditLog do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          user_id: String.t() | nil,
          project_id: String.t() | nil,
          organization_id: String.t() | nil,
          action: String.t(),
          args: map() | nil,
          error: String.t() | nil,
          ip_address: String.t() | nil,
          user_agent: String.t() | nil,
          metadata: map() | nil,
          actor_user_id: String.t() | nil,
          target_kind: String.t() | nil,
          target_id: String.t() | nil,
          before: map() | nil,
          after: map() | nil,
          idempotency_key: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "AuditLog" do
    field(:id, :string, primary_key: true)
    field(:user_id, :string, source: :userId)
    field(:project_id, :string, source: :projectId)
    field(:organization_id, :string, source: :organizationId)
    field(:action, :string)
    field(:args, :map)
    field(:error, :string)
    field(:ip_address, :string, source: :ipAddress)
    field(:user_agent, :string, source: :userAgent)
    field(:metadata, :map)
    field(:actor_user_id, :string, source: :actorUserId)
    field(:target_kind, :string, source: :targetKind)
    field(:target_id, :string, source: :targetId)
    field(:before, :map)
    field(:after, :map)
    field(:idempotency_key, :string, source: :idempotencyKey)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end