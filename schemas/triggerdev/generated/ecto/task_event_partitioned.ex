defmodule Trigger.TaskEventPartitioned do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          message: String.t(),
          trace_id: String.t(),
          span_id: String.t(),
          parent_id: String.t() | nil,
          tracestate: String.t() | nil,
          is_error: boolean(),
          is_partial: boolean(),
          is_cancelled: boolean(),
          service_name: String.t(),
          service_namespace: String.t(),
          level: atom(),
          kind: atom(),
          status: atom(),
          links: map() | nil,
          events: map() | nil,
          start_time: integer(),
          duration: integer(),
          attempt_id: String.t() | nil,
          attempt_number: integer() | nil,
          environment_id: String.t(),
          environment_type: atom(),
          organization_id: String.t(),
          project_id: String.t(),
          project_ref: String.t(),
          run_id: String.t(),
          run_is_test: boolean(),
          idempotency_key: String.t() | nil,
          task_slug: String.t(),
          task_path: String.t() | nil,
          task_export_name: String.t() | nil,
          worker_id: String.t() | nil,
          worker_version: String.t() | nil,
          queue_id: String.t() | nil,
          queue_name: String.t() | nil,
          batch_id: String.t() | nil,
          properties: map(),
          metadata: map() | nil,
          style: map() | nil,
          output: map() | nil,
          output_type: String.t() | nil,
          payload: map() | nil,
          payload_type: String.t() | nil,
          usage_duration_ms: integer(),
          usage_cost_in_cents: float(),
          machine_preset: String.t() | nil,
          machine_preset_cpu: float() | nil,
          machine_preset_memory: float() | nil,
          machine_preset_cents_per_ms: float() | nil,
          inserted_at: DateTime.t()
        }

  schema "TaskEventPartitioned" do
    field(:id, :string, primary_key: true)
    field(:message, :string)
    field(:trace_id, :string, source: :traceId)
    field(:span_id, :string, source: :spanId)
    field(:parent_id, :string, source: :parentId)
    field(:tracestate, :string)
    field(:is_error, :boolean, default: false, source: :isError)
    field(:is_partial, :boolean, default: false, source: :isPartial)
    field(:is_cancelled, :boolean, default: false, source: :isCancelled)
    field(:service_name, :string, source: :serviceName)
    field(:service_namespace, :string, source: :serviceNamespace)
    field(:level, Ecto.Enum, values: [:TRACE, :DEBUG, :LOG, :INFO, :WARN, :ERROR], default: :TRACE)
    field(:kind, Ecto.Enum, values: [:UNSPECIFIED, :INTERNAL, :SERVER, :CLIENT, :PRODUCER, :CONSUMER, :UNRECOGNIZED, :LOG], default: :INTERNAL)
    field(:status, Ecto.Enum, values: [:UNSET, :OK, :ERROR, :UNRECOGNIZED], default: :UNSET)
    field(:links, :map)
    field(:events, :map)
    field(:start_time, :integer, source: :startTime)
    field(:duration, :integer, default: 0)
    field(:attempt_id, :string, source: :attemptId)
    field(:attempt_number, :integer, source: :attemptNumber)
    field(:environment_id, :string, source: :environmentId)
    field(:environment_type, Ecto.Enum, values: [:PRODUCTION, :STAGING, :DEVELOPMENT, :PREVIEW], source: :environmentType)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:project_ref, :string, source: :projectRef)
    field(:run_id, :string, source: :runId)
    field(:run_is_test, :boolean, default: false, source: :runIsTest)
    field(:idempotency_key, :string, source: :idempotencyKey)
    field(:task_slug, :string, source: :taskSlug)
    field(:task_path, :string, source: :taskPath)
    field(:task_export_name, :string, source: :taskExportName)
    field(:worker_id, :string, source: :workerId)
    field(:worker_version, :string, source: :workerVersion)
    field(:queue_id, :string, source: :queueId)
    field(:queue_name, :string, source: :queueName)
    field(:batch_id, :string, source: :batchId)
    field(:properties, :map)
    field(:metadata, :map)
    field(:style, :map)
    field(:output, :map)
    field(:output_type, :string, source: :outputType)
    field(:payload, :map)
    field(:payload_type, :string, source: :payloadType)
    field(:usage_duration_ms, :integer, default: 0, source: :usageDurationMs)
    field(:usage_cost_in_cents, :float, default: 0.0, source: :usageCostInCents)
    field(:machine_preset, :string, source: :machinePreset)
    field(:machine_preset_cpu, :float, source: :machinePresetCpu)
    field(:machine_preset_memory, :float, source: :machinePresetMemory)
    field(:machine_preset_cents_per_ms, :float, source: :machinePresetCentsPerMs)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end