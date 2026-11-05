defmodule InboxZero.OrganizationRule do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          enabled: boolean(),
          name: String.t(),
          instructions: String.t() | nil,
          run_on_threads: boolean(),
          conditional_operator: atom(),
          from: String.t() | nil,
          to: String.t() | nil,
          subject: String.t() | nil,
          body: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: InboxZero.Organization.t() | nil,
          actions: [InboxZero.OrganizationRuleAction.t()],
          member_rules: [InboxZero.Rule.t()]
        }

  schema "OrganizationRule" do
    field(:id, :string, primary_key: true)
    field(:enabled, :boolean, default: true)
    field(:name, :string)
    field(:instructions, :string)
    field(:run_on_threads, :boolean, default: false, source: :runOnThreads)
    field(:conditional_operator, Ecto.Enum, values: [:AND, :OR], default: :AND, source: :conditionalOperator)
    field(:from, :string)
    field(:to, :string)
    field(:subject, :string)
    field(:body, :string)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, InboxZero.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:actions, InboxZero.OrganizationRuleAction, foreign_key: :organization_rule_id, references: :id)
    has_many(:member_rules, InboxZero.Rule, foreign_key: :organization_rule_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end