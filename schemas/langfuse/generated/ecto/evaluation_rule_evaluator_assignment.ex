defmodule Langfuse.EvaluationRuleEvaluatorAssignment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          variable_mapping: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          evaluation_rule: Langfuse.EvaluationRule.t() | nil,
          evaluator: Langfuse.Evaluator.t() | nil
        }

  schema "evaluation_rule_evaluator_assignments" do
    field(:id, :string, primary_key: true)
    field(:variable_mapping, :map)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:evaluation_rule, Langfuse.EvaluationRule, foreign_key: :evaluation_rule_id, type: :string)
    belongs_to(:evaluator, Langfuse.Evaluator, foreign_key: :evaluator_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end