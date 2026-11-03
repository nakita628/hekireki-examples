defmodule Formbricks.OrganizationBilling do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          stripe_customer_id: String.t() | nil,
          limits: map(),
          usage_cycle_anchor: DateTime.t() | nil,
          stripe: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Formbricks.Organization.t() | nil
        }

  schema "OrganizationBilling" do
    field(:stripe_customer_id, :string)
    field(:limits, :map)
    field(:usage_cycle_anchor, Formbricks.PrismaDateTime)
    field(:stripe, :map)
    belongs_to(:organization, Formbricks.Organization, foreign_key: :organization_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end