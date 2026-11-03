defmodule Dub.Customer do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          email: String.t() | nil,
          normalized_email: String.t() | nil,
          avatar: String.t() | nil,
          external_id: String.t() | nil,
          stripe_customer_id: String.t() | nil,
          click_id: String.t() | nil,
          clicked_at: DateTime.t() | nil,
          country: String.t() | nil,
          sales: integer(),
          sale_amount: integer(),
          first_sale_at: DateTime.t() | nil,
          subscription_canceled_at: DateTime.t() | nil,
          project_connect_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Dub.Project.t() | nil,
          link: Dub.Link.t() | nil
        }

  schema "Customer" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:email, :string)
    field(:normalized_email, :string, source: :normalizedEmail)
    field(:avatar, :string)
    field(:external_id, :string, source: :externalId)
    field(:stripe_customer_id, :string, source: :stripeCustomerId)
    field(:click_id, :string, source: :clickId)
    field(:clicked_at, Dub.PrismaDateTime, source: :clickedAt)
    field(:country, :string)
    field(:sales, :integer, default: 0)
    field(:sale_amount, :integer, default: 0, source: :saleAmount)
    field(:first_sale_at, Dub.PrismaDateTime, source: :firstSaleAt)
    field(:subscription_canceled_at, Dub.PrismaDateTime, source: :subscriptionCanceledAt)
    field(:project_connect_id, :string, source: :projectConnectId)
    field(:project_id, :string, source: :projectId)
    field(:link_id, :string, source: :linkId)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:link, Dub.Link, foreign_key: :link_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end