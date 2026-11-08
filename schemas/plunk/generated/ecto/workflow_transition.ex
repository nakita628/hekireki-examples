defmodule Plunk.WorkflowTransition do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          condition: map() | nil,
          priority: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          from_step: Plunk.WorkflowStep.t() | nil,
          to_step: Plunk.WorkflowStep.t() | nil
        }

  schema "workflow_transitions" do
    field(:condition, :map)
    field(:priority, :integer, default: 0)
    field(:from_step_id, :binary_id, source: :fromStepId)
    field(:to_step_id, :binary_id, source: :toStepId)
    belongs_to(:from_step, Plunk.WorkflowStep, foreign_key: :from_step_id, define_field: false)
    belongs_to(:to_step, Plunk.WorkflowStep, foreign_key: :to_step_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end