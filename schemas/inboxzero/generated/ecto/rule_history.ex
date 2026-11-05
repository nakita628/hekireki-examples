defmodule InboxZero.RuleHistory do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          version: integer(),
          trigger_type: String.t(),
          prompt_text: String.t() | nil,
          name: String.t(),
          instructions: String.t() | nil,
          enabled: boolean(),
          automate: boolean(),
          run_on_threads: boolean(),
          conditional_operator: String.t(),
          from: String.t() | nil,
          to: String.t() | nil,
          subject: String.t() | nil,
          body: String.t() | nil,
          category_filter_type: String.t() | nil,
          system_type: String.t() | nil,
          actions: map(),
          category_filters: map() | nil,
          inserted_at: DateTime.t(),
          rule: InboxZero.Rule.t() | nil
        }

  schema "RuleHistory" do
    field(:id, :string, primary_key: true)
    field(:version, :integer)
    field(:trigger_type, :string, source: :triggerType)
    field(:prompt_text, :string, source: :promptText)
    field(:name, :string)
    field(:instructions, :string)
    field(:enabled, :boolean)
    field(:automate, :boolean)
    field(:run_on_threads, :boolean, source: :runOnThreads)
    field(:conditional_operator, :string, source: :conditionalOperator)
    field(:from, :string)
    field(:to, :string)
    field(:subject, :string)
    field(:body, :string)
    field(:category_filter_type, :string, source: :categoryFilterType)
    field(:system_type, :string, source: :systemType)
    field(:actions, :map)
    field(:category_filters, :map, source: :categoryFilters)
    field(:rule_id, :string, source: :ruleId)
    belongs_to(:rule, InboxZero.Rule, foreign_key: :rule_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end