defmodule Calcom.App do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          slug: String.t(),
          dir_name: String.t(),
          keys: map() | nil,
          categories: atom(),
          enabled: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          credentials: [Calcom.Credential.t()],
          payments: [Calcom.Payment.t()],
          webhook: [Calcom.Webhook.t()],
          api_key: [Calcom.ApiKey.t()]
        }

  schema "App" do
    field(:slug, :string, primary_key: true)
    field(:dir_name, :string, source: :dirName)
    field(:keys, :map)
    field(:categories, {:array, Ecto.Enum}, values: [:calendar, :messaging, :other, :payment, :video, :web3, :automation, :analytics, :conferencing, :crm])
    field(:enabled, :boolean, default: false)
    has_many(:credentials, Calcom.Credential, foreign_key: :app_id, references: :slug)
    has_many(:payments, Calcom.Payment, foreign_key: :app_id, references: :slug)
    has_many(:webhook, Calcom.Webhook, foreign_key: :app_id, references: :slug)
    has_many(:api_key, Calcom.ApiKey, foreign_key: :app_id, references: :slug)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end