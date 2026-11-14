defmodule LangWatch.RoutingPolicy do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          model_provider_ids: map(),
          model_aliases: map(),
          default_model: String.t() | nil,
          policy_rules: map(),
          is_default: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          created_by: LangWatch.User.t() | nil,
          updated_by: LangWatch.User.t() | nil,
          virtual_keys: [LangWatch.VirtualKey.t()],
          scopes: [LangWatch.RoutingPolicyScope.t()]
        }

  schema "RoutingPolicy" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:model_provider_ids, :map, source: :modelProviderIds)
    field(:model_aliases, :map, default: %{}, source: :modelAliases)
    field(:default_model, :string, source: :defaultModel)
    field(:policy_rules, :map, default: %{}, source: :policyRules)
    field(:is_default, :boolean, default: false, source: :isDefault)
    field(:organization_id, :string, source: :organizationId)
    field(:created_by_id, :string, source: :createdById)
    field(:updated_by_id, :string, source: :updatedById)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:created_by, LangWatch.User, foreign_key: :created_by_id, define_field: false, type: :string)
    belongs_to(:updated_by, LangWatch.User, foreign_key: :updated_by_id, define_field: false, type: :string)
    has_many(:virtual_keys, LangWatch.VirtualKey, foreign_key: :routing_policy_id, references: :id)
    has_many(:scopes, LangWatch.RoutingPolicyScope, foreign_key: :routing_policy_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end