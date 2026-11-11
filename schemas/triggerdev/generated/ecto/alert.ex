defmodule Trigger.Alert do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          project_id: String.t(),
          environment_id: String.t(),
          capability_key: String.t(),
          params: map(),
          condition: map(),
          query: String.t() | nil,
          expires_at: DateTime.t() | nil,
          lifecycle: String.t(),
          attention: String.t(),
          observed_condition: String.t(),
          observed_condition_changed_at: DateTime.t() | nil,
          pending_met_since: DateTime.t() | nil,
          evaluator_state: map() | nil,
          evaluation_lease_id: String.t() | nil,
          evaluation_lease_until: DateTime.t() | nil,
          last_evaluated_at: DateTime.t() | nil,
          next_evaluation_at: DateTime.t() | nil,
          notifiers: map(),
          name: String.t() | nil,
          created_by: String.t(),
          updated_at: DateTime.t(),
          resolved_at: DateTime.t() | nil,
          resolved_by: String.t() | nil,
          stopped_at: DateTime.t() | nil,
          stopped_by: String.t() | nil,
          last_occurrence_at: DateTime.t() | nil,
          occurrences_since_raise: integer(),
          occurrences_total: integer(),
          group_id: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "Alert" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:environment_id, :string, source: :environmentId)
    field(:capability_key, :string, source: :capabilityKey)
    field(:params, :map)
    field(:condition, :map)
    field(:query, :string)
    field(:expires_at, Trigger.PrismaDateTime, source: :expiresAt)
    field(:lifecycle, :string, default: "active")
    field(:attention, :string, default: "clear")
    field(:observed_condition, :string, default: "unknown", source: :observedCondition)
    field(:observed_condition_changed_at, Trigger.PrismaDateTime, source: :observedConditionChangedAt)
    field(:pending_met_since, Trigger.PrismaDateTime, source: :pendingMetSince)
    field(:evaluator_state, :map, source: :evaluatorState)
    field(:evaluation_lease_id, :string, source: :evaluationLeaseId)
    field(:evaluation_lease_until, Trigger.PrismaDateTime, source: :evaluationLeaseUntil)
    field(:last_evaluated_at, Trigger.PrismaDateTime, source: :lastEvaluatedAt)
    field(:next_evaluation_at, Trigger.PrismaDateTime, source: :nextEvaluationAt)
    field(:notifiers, :map)
    field(:name, :string)
    field(:created_by, :string, source: :createdBy)
    field(:updated_at, Trigger.PrismaDateTime, autogenerate: true, source: :updatedAt)
    field(:resolved_at, Trigger.PrismaDateTime, source: :resolvedAt)
    field(:resolved_by, :string, source: :resolvedBy)
    field(:stopped_at, Trigger.PrismaDateTime, source: :stoppedAt)
    field(:stopped_by, :string, source: :stoppedBy)
    field(:last_occurrence_at, Trigger.PrismaDateTime, source: :lastOccurrenceAt)
    field(:occurrences_since_raise, :integer, default: 0, source: :occurrencesSinceRaise)
    field(:occurrences_total, :integer, default: 0, source: :occurrencesTotal)
    field(:group_id, :string, source: :groupId)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end