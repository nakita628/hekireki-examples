defmodule InboxZero.ScimConnectionBinding do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          connection_id: String.t(),
          connection_key: String.t(),
          provisioning_domain_id: String.t(),
          created_at: DateTime.t(),
          decommissioned_at: DateTime.t() | nil,
          decommission_status: String.t(),
          decommission_cursor_user_id: String.t() | nil,
          decommission_reconciled_user_count: integer(),
          decommission_batch_count: integer(),
          decommission_revision: integer(),
          decommission_completed_at: DateTime.t() | nil,
          decommission_lease_id: String.t() | nil,
          decommission_lease_expires_at: DateTime.t() | nil
        }

  schema "scimConnectionBinding" do
    field(:id, :string, primary_key: true)
    field(:connection_id, :string, source: :connectionId)
    field(:connection_key, :string, source: :connectionKey)
    field(:provisioning_domain_id, :string, source: :provisioningDomainId)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:decommissioned_at, InboxZero.PrismaDateTime, source: :decommissionedAt)
    field(:decommission_status, :string, default: "active", source: :decommissionStatus)
    field(:decommission_cursor_user_id, :string, source: :decommissionCursorUserId)
    field(:decommission_reconciled_user_count, :integer, default: 0, source: :decommissionReconciledUserCount)
    field(:decommission_batch_count, :integer, default: 0, source: :decommissionBatchCount)
    field(:decommission_revision, :integer, default: 0, source: :decommissionRevision)
    field(:decommission_completed_at, InboxZero.PrismaDateTime, source: :decommissionCompletedAt)
    field(:decommission_lease_id, :string, source: :decommissionLeaseId)
    field(:decommission_lease_expires_at, InboxZero.PrismaDateTime, source: :decommissionLeaseExpiresAt)
  end
end