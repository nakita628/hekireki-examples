defmodule Postiz.IntegrationsWebhooks do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          integration_id: String.t(),
          webhook_id: Ecto.UUID.t(),
          integration: Postiz.Integration.t() | nil,
          webhook: Postiz.Webhooks.t() | nil
        }

  schema "IntegrationsWebhooks" do
    field(:integration_id, :string, primary_key: true, source: :integrationId)
    field(:webhook_id, :binary_id, primary_key: true, source: :webhookId)
    belongs_to(:integration, Postiz.Integration, foreign_key: :integration_id, define_field: false, type: :string)
    belongs_to(:webhook, Postiz.Webhooks, foreign_key: :webhook_id, define_field: false, type: :binary_id)
  end
end