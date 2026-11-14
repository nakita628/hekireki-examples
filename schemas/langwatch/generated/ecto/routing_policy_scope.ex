defmodule LangWatch.RoutingPolicyScope do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          scope_type: atom(),
          scope_id: String.t(),
          inserted_at: DateTime.t(),
          routing_policy: LangWatch.RoutingPolicy.t() | nil
        }

  schema "RoutingPolicyScope" do
    field(:id, :string, primary_key: true)
    field(:scope_type, Ecto.Enum, values: [:ORGANIZATION, :TEAM, :PROJECT], source: :scopeType)
    field(:scope_id, :string, source: :scopeId)
    field(:routing_policy_id, :string, source: :routingPolicyId)
    belongs_to(:routing_policy, LangWatch.RoutingPolicy, foreign_key: :routing_policy_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end