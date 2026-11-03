defmodule Documenso.Webhook do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          webhook_url: String.t(),
          event_triggers: atom(),
          secret: String.t() | nil,
          enabled: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Documenso.User.t() | nil,
          team: Documenso.Team.t() | nil,
          webhook_calls: [Documenso.WebhookCall.t()]
        }

  schema "Webhook" do
    field(:id, :string, primary_key: true)
    field(:webhook_url, :string, source: :webhookUrl)
    field(:event_triggers, {:array, Ecto.Enum}, values: [:DOCUMENT_CREATED, :DOCUMENT_SENT, :DOCUMENT_OPENED, :DOCUMENT_SIGNED, :DOCUMENT_COMPLETED, :DOCUMENT_REJECTED, :DOCUMENT_CANCELLED, :RECIPIENT_EXPIRED, :DOCUMENT_RECIPIENT_COMPLETED, :DOCUMENT_REMINDER_SENT, :TEMPLATE_CREATED, :TEMPLATE_UPDATED, :TEMPLATE_DELETED, :TEMPLATE_USED], source: :eventTriggers)
    field(:secret, :string)
    field(:enabled, :boolean, default: true)
    field(:user_id, :id, source: :userId)
    field(:team_id, :id, source: :teamId)
    belongs_to(:user, Documenso.User, foreign_key: :user_id, define_field: false)
    belongs_to(:team, Documenso.Team, foreign_key: :team_id, define_field: false)
    has_many(:webhook_calls, Documenso.WebhookCall, foreign_key: :webhook_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end