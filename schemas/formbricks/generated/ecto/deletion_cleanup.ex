defmodule Formbricks.DeletionCleanup do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          kind: atom(),
          organization_id: String.t(),
          workspace_id: String.t(),
          survey_id: String.t(),
          tenant_ids: [String.t()],
          response_ids: [String.t()],
          file_keys: [String.t()],
          attempts: integer(),
          next_attempt_at: DateTime.t(),
          last_error: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "DeletionCleanup" do
    field(:id, :string, primary_key: true)
    field(:kind, Ecto.Enum, values: [:hubSurvey, :hubResponses, :storageFiles, :storageSurveyFolder, :hubResponseOutcome])
    field(:organization_id, :string, source: :organizationId)
    field(:workspace_id, :string, source: :workspaceId)
    field(:survey_id, :string, source: :surveyId)
    field(:tenant_ids, {:array, :string}, source: :tenantIds)
    field(:response_ids, {:array, :string}, source: :responseIds)
    field(:file_keys, {:array, :string}, source: :fileKeys)
    field(:attempts, :integer, default: 0)
    field(:next_attempt_at, Formbricks.PrismaDateTime, autogenerate: true, source: :nextAttemptAt)
    field(:last_error, :string, source: :lastError)
    timestamps(inserted_at_source: :created_at)
  end
end