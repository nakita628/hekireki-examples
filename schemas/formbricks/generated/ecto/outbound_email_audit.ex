defmodule Formbricks.OutboundEmailAudit do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          expires_at: DateTime.t(),
          content_expires_at: DateTime.t(),
          environment: String.t(),
          email_type: String.t(),
          trigger: String.t(),
          actor_kind: String.t(),
          actor_user_id: String.t() | nil,
          configured_by_user_id: String.t() | nil,
          actor_user_hash: String.t() | nil,
          configured_by_user_hash: String.t() | nil,
          organization_id: String.t() | nil,
          survey_id: String.t() | nil,
          response_id: String.t() | nil,
          follow_up_id: String.t() | nil,
          workflow_id: String.t() | nil,
          workflow_run_id: String.t() | nil,
          invite_id: String.t() | nil,
          recipient_mac: String.t() | nil,
          ses_message_hash: String.t() | nil,
          snapshot_key: String.t() | nil,
          snapshot_sha256: String.t() | nil,
          status: String.t(),
          status_recorded_at: DateTime.t() | nil,
          audit_gap: boolean(),
          erasure_requested_at: DateTime.t() | nil,
          erased_at: DateTime.t() | nil,
          content_delete_retry_at: DateTime.t() | nil,
          content_delete_failures: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "OutboundEmailAudit" do
    field(:expires_at, Formbricks.PrismaDateTime, source: :expiresAt)
    field(:content_expires_at, Formbricks.PrismaDateTime, source: :contentExpiresAt)
    field(:environment, :string)
    field(:email_type, :string, source: :emailType)
    field(:trigger, :string)
    field(:actor_kind, :string, source: :actorKind)
    field(:actor_user_id, :string, source: :actorUserId)
    field(:configured_by_user_id, :string, source: :configuredByUserId)
    field(:actor_user_hash, :string, source: :actorUserHash)
    field(:configured_by_user_hash, :string, source: :configuredByUserHash)
    field(:organization_id, :string, source: :organizationId)
    field(:survey_id, :string, source: :surveyId)
    field(:response_id, :string, source: :responseId)
    field(:follow_up_id, :string, source: :followUpId)
    field(:workflow_id, :string, source: :workflowId)
    field(:workflow_run_id, :string, source: :workflowRunId)
    field(:invite_id, :string, source: :inviteId)
    field(:recipient_mac, :string, source: :recipientMac)
    field(:ses_message_hash, :string, source: :sesMessageHash)
    field(:snapshot_key, :string, source: :snapshotKey)
    field(:snapshot_sha256, :string, source: :snapshotSha256)
    field(:status, :string, default: "prepared")
    field(:status_recorded_at, Formbricks.PrismaDateTime, source: :statusRecordedAt)
    field(:audit_gap, :boolean, default: false, source: :auditGap)
    field(:erasure_requested_at, Formbricks.PrismaDateTime, source: :erasureRequestedAt)
    field(:erased_at, Formbricks.PrismaDateTime, source: :erasedAt)
    field(:content_delete_retry_at, Formbricks.PrismaDateTime, source: :contentDeleteRetryAt)
    field(:content_delete_failures, :integer, default: 0, source: :contentDeleteFailures)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end