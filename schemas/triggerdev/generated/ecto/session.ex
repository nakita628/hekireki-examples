defmodule Trigger.Session do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          external_id: String.t() | nil,
          type: String.t(),
          project_id: String.t(),
          runtime_environment_id: String.t(),
          environment_type: atom(),
          organization_id: String.t(),
          task_identifier: String.t(),
          trigger_config: map(),
          is_test: boolean(),
          tags: [String.t()],
          metadata: map() | nil,
          current_run_id: String.t() | nil,
          current_run_version: integer(),
          closed_at: DateTime.t() | nil,
          closed_reason: String.t() | nil,
          expires_at: DateTime.t() | nil,
          stream_basin_name: String.t() | nil,
          chat_snapshot_storage_path: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          runs: [Trigger.SessionRun.t()]
        }

  schema "Session" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:external_id, :string, source: :externalId)
    field(:type, :string)
    field(:project_id, :string, source: :projectId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    field(:environment_type, Ecto.Enum, values: [:PRODUCTION, :STAGING, :DEVELOPMENT, :PREVIEW], source: :environmentType)
    field(:organization_id, :string, source: :organizationId)
    field(:task_identifier, :string, source: :taskIdentifier)
    field(:trigger_config, :map, source: :triggerConfig)
    field(:is_test, :boolean, default: false, source: :isTest)
    field(:tags, {:array, :string})
    field(:metadata, :map)
    field(:current_run_id, :string, source: :currentRunId)
    field(:current_run_version, :integer, default: 0, source: :currentRunVersion)
    field(:closed_at, Trigger.PrismaDateTime, source: :closedAt)
    field(:closed_reason, :string, source: :closedReason)
    field(:expires_at, Trigger.PrismaDateTime, source: :expiresAt)
    field(:stream_basin_name, :string, source: :streamBasinName)
    field(:chat_snapshot_storage_path, :string, source: :chatSnapshotStoragePath)
    has_many(:runs, Trigger.SessionRun, foreign_key: :session_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end