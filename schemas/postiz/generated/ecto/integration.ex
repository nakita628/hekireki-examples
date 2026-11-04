defmodule Postiz.Integration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          internal_id: String.t(),
          name: String.t(),
          custom_name: String.t() | nil,
          picture: String.t() | nil,
          provider_identifier: String.t(),
          type: String.t(),
          token: String.t(),
          disabled: boolean(),
          token_expiration: DateTime.t() | nil,
          refresh_token: String.t() | nil,
          profile: String.t() | nil,
          deleted_at: DateTime.t() | nil,
          in_between_steps: boolean(),
          refresh_needed: boolean(),
          posting_times: String.t(),
          custom_instance_details: String.t() | nil,
          root_internal_id: String.t() | nil,
          additional_settings: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t() | nil,
          customer: Postiz.Customer.t() | nil,
          organization: Postiz.Organization.t() | nil,
          exising_plug_data: [Postiz.ExisingPlugData.t()],
          webhooks: [Postiz.IntegrationsWebhooks.t()],
          order_items: [Postiz.OrderItems.t()],
          plugs: [Postiz.Plugs.t()],
          posts: [Postiz.Post.t()]
        }

  schema "Integration" do
    field(:id, :string, primary_key: true)
    field(:internal_id, :string, source: :internalId)
    field(:name, :string)
    field(:custom_name, :string, source: :customName)
    field(:picture, :string)
    field(:provider_identifier, :string, source: :providerIdentifier)
    field(:type, :string)
    field(:token, :string)
    field(:disabled, :boolean, default: false)
    field(:token_expiration, Postiz.PrismaDateTime, source: :tokenExpiration)
    field(:refresh_token, :string, source: :refreshToken)
    field(:profile, :string)
    field(:deleted_at, Postiz.PrismaDateTime, source: :deletedAt)
    field(:in_between_steps, :boolean, default: false, source: :inBetweenSteps)
    field(:refresh_needed, :boolean, default: false, source: :refreshNeeded)
    field(:posting_times, :string, default: "[{\"time\":120}, {\"time\":400}, {\"time\":700}]", source: :postingTimes)
    field(:custom_instance_details, :string, source: :customInstanceDetails)
    field(:root_internal_id, :string, source: :rootInternalId)
    field(:additional_settings, :string, default: "[]", source: :additionalSettings)
    field(:customer_id, :binary_id, source: :customerId)
    field(:organization_id, :binary_id, source: :organizationId)
    belongs_to(:customer, Postiz.Customer, foreign_key: :customer_id, define_field: false, type: :binary_id)
    belongs_to(:organization, Postiz.Organization, foreign_key: :organization_id, define_field: false, type: :binary_id)
    has_many(:exising_plug_data, Postiz.ExisingPlugData, foreign_key: :integration_id, references: :id)
    has_many(:webhooks, Postiz.IntegrationsWebhooks, foreign_key: :integration_id, references: :id)
    has_many(:order_items, Postiz.OrderItems, foreign_key: :integration_id, references: :id)
    has_many(:plugs, Postiz.Plugs, foreign_key: :integration_id, references: :id)
    has_many(:posts, Postiz.Post, foreign_key: :integration_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end