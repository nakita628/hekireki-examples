defmodule Postiz.Subscription do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          subscription_tier: atom(),
          provider: String.t(),
          identifier: String.t() | nil,
          cancel_at: DateTime.t() | nil,
          period: atom(),
          total_channels: integer(),
          is_lifetime: boolean(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Postiz.Organization.t() | nil
        }

  schema "Subscription" do
    field(:id, :string, primary_key: true)
    field(:subscription_tier, Ecto.Enum, values: [:STANDARD, :PRO, :TEAM, :ULTIMATE], source: :subscriptionTier)
    field(:provider, :string, default: "stripe")
    field(:identifier, :string)
    field(:cancel_at, Postiz.PrismaDateTime, source: :cancelAt)
    field(:period, Ecto.Enum, values: [:MONTHLY, :YEARLY])
    field(:total_channels, :integer, source: :totalChannels)
    field(:is_lifetime, :boolean, default: false, source: :isLifetime)
    field(:deleted_at, Postiz.PrismaDateTime, source: :deletedAt)
    field(:organization_id, :binary_id, source: :organizationId)
    belongs_to(:organization, Postiz.Organization, foreign_key: :organization_id, define_field: false, type: :binary_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end