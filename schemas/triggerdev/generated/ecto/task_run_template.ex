defmodule Trigger.TaskRunTemplate do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          task_slug: String.t(),
          trigger_source: atom(),
          label: String.t(),
          payload: String.t() | nil,
          payload_type: String.t(),
          metadata: String.t() | nil,
          metadata_type: String.t(),
          queue: String.t() | nil,
          concurrency_key: String.t() | nil,
          ttl_seconds: integer() | nil,
          max_attempts: integer() | nil,
          max_duration_seconds: integer() | nil,
          tags: [String.t()],
          machine_preset: String.t() | nil,
          project_id: String.t(),
          organization_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "TaskRunTemplate" do
    field(:id, :string, primary_key: true)
    field(:task_slug, :string, source: :taskSlug)
    field(:trigger_source, Ecto.Enum, values: [:STANDARD, :SCHEDULED, :AGENT, :WEBHOOK], source: :triggerSource)
    field(:label, :string)
    field(:payload, :string)
    field(:payload_type, :string, default: "application/json", source: :payloadType)
    field(:metadata, :string)
    field(:metadata_type, :string, default: "application/json", source: :metadataType)
    field(:queue, :string)
    field(:concurrency_key, :string, source: :concurrencyKey)
    field(:ttl_seconds, :integer, source: :ttlSeconds)
    field(:max_attempts, :integer, source: :maxAttempts)
    field(:max_duration_seconds, :integer, source: :maxDurationSeconds)
    field(:tags, {:array, :string})
    field(:machine_preset, :string, source: :machinePreset)
    field(:project_id, :string, source: :projectId)
    field(:organization_id, :string, source: :organizationId)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end