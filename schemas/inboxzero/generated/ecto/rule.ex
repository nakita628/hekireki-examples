defmodule InboxZero.Rule do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          enabled: boolean(),
          automate: boolean(),
          run_on_threads: boolean(),
          organization_rule_member_enabled: boolean() | nil,
          conditional_operator: atom(),
          instructions: String.t() | nil,
          from: String.t() | nil,
          to: String.t() | nil,
          subject: String.t() | nil,
          body: String.t() | nil,
          category_filter_type: atom() | nil,
          system_type: atom() | nil,
          prompt_text: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          organization_rule: InboxZero.OrganizationRule.t() | nil,
          group: InboxZero.Group.t() | nil,
          attachment_sources: [InboxZero.AttachmentSource.t()],
          executed_rules: [InboxZero.ExecutedRule.t()],
          classification_feedback: [InboxZero.ClassificationFeedback.t()],
          history: [InboxZero.RuleHistory.t()],
          category_filters: [InboxZero.Category.t()]
        }

  schema "Rule" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:enabled, :boolean, default: true)
    field(:automate, :boolean, default: true)
    field(:run_on_threads, :boolean, default: false, source: :runOnThreads)
    field(:organization_rule_member_enabled, :boolean, source: :organizationRuleMemberEnabled)
    field(:conditional_operator, Ecto.Enum, values: [:AND, :OR], default: :AND, source: :conditionalOperator)
    field(:instructions, :string)
    field(:from, :string)
    field(:to, :string)
    field(:subject, :string)
    field(:body, :string)
    field(:category_filter_type, Ecto.Enum, values: [:INCLUDE, :EXCLUDE], source: :categoryFilterType)
    field(:system_type, Ecto.Enum, values: [:TO_REPLY, :FYI, :AWAITING_REPLY, :ACTIONED, :COLD_EMAIL, :NEWSLETTER, :MARKETING, :CALENDAR, :RECEIPT, :NOTIFICATION, :OTP], source: :systemType)
    field(:prompt_text, :string, source: :promptText)
    field(:email_account_id, :string, source: :emailAccountId)
    field(:organization_rule_id, :string, source: :organizationRuleId)
    field(:group_id, :string, source: :groupId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    belongs_to(:organization_rule, InboxZero.OrganizationRule, foreign_key: :organization_rule_id, define_field: false, type: :string)
    belongs_to(:group, InboxZero.Group, foreign_key: :group_id, define_field: false, type: :string)
    has_many(:attachment_sources, InboxZero.AttachmentSource, foreign_key: :rule_id, references: :id)
    has_many(:executed_rules, InboxZero.ExecutedRule, foreign_key: :rule_id, references: :id)
    has_many(:classification_feedback, InboxZero.ClassificationFeedback, foreign_key: :rule_id, references: :id)
    has_many(:history, InboxZero.RuleHistory, foreign_key: :rule_id, references: :id)
    many_to_many(:category_filters, InboxZero.Category, join_through: "_CategoryToRule", join_keys: [B: :id, A: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end