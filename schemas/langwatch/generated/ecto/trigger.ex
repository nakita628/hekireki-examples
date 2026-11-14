defmodule LangWatch.Trigger do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          action: atom(),
          trigger_kind: atom(),
          action_params: map(),
          filters: map(),
          filter_query: String.t() | nil,
          last_run_at: float(),
          active: boolean(),
          paused_reason: String.t() | nil,
          paused_at: DateTime.t() | nil,
          message: String.t() | nil,
          deleted: boolean(),
          alert_type: atom() | nil,
          slack_template_type: String.t() | nil,
          slack_template: String.t() | nil,
          email_subject_template: String.t() | nil,
          email_body_template: String.t() | nil,
          notification_cadence: String.t(),
          trace_debounce_ms: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          custom_graph: LangWatch.CustomGraph.t() | nil,
          latest_evaluation: LangWatch.TriggerLatestEvaluation.t() | nil,
          trigger_sent: [LangWatch.TriggerSent.t()],
          webhook_deliveries: [LangWatch.WebhookEndpointDelivery.t()]
        }

  schema "Trigger" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:action, Ecto.Enum, values: [:SEND_EMAIL, :ADD_TO_DATASET, :ADD_TO_ANNOTATION_QUEUE, :SEND_SLACK_MESSAGE, :SEND_WEBHOOK])
    field(:trigger_kind, Ecto.Enum, values: [:AUTOMATION, :ALERT, :REPORT], default: :AUTOMATION, source: :triggerKind)
    field(:action_params, :map, source: :actionParams)
    field(:filters, :map)
    field(:filter_query, :string, source: :filterQuery)
    field(:last_run_at, :float, default: 0.0, source: :lastRunAt)
    field(:active, :boolean, default: true)
    field(:paused_reason, :string, source: :pausedReason)
    field(:paused_at, LangWatch.PrismaDateTime, source: :pausedAt)
    field(:message, :string)
    field(:deleted, :boolean, default: false)
    field(:alert_type, Ecto.Enum, values: [:CRITICAL, :WARNING, :INFO], source: :alertType)
    field(:slack_template_type, :string, source: :slackTemplateType)
    field(:slack_template, :string, source: :slackTemplate)
    field(:email_subject_template, :string, source: :emailSubjectTemplate)
    field(:email_body_template, :string, source: :emailBodyTemplate)
    field(:notification_cadence, :string, default: "immediate", source: :notificationCadence)
    field(:trace_debounce_ms, :integer, default: 30000, source: :traceDebounceMs)
    field(:project_id, :string, source: :projectId)
    field(:custom_graph_id, :string, source: :customGraphId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:custom_graph, LangWatch.CustomGraph, foreign_key: :custom_graph_id, define_field: false, type: :string)
    has_one(:latest_evaluation, LangWatch.TriggerLatestEvaluation, foreign_key: :trigger_id, references: :id)
    has_many(:trigger_sent, LangWatch.TriggerSent, foreign_key: :trigger_id, references: :id)
    has_many(:webhook_deliveries, LangWatch.WebhookEndpointDelivery, foreign_key: :trigger_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end