defmodule Papermark.IncomingWebhook do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          external_id: String.t(),
          name: String.t(),
          secret: String.t() | nil,
          source: String.t() | nil,
          actions: String.t() | nil,
          consecutive_failures: integer(),
          last_failed_at: DateTime.t() | nil,
          disabled_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Papermark.Team.t() | nil
        }

  schema "IncomingWebhook" do
    field(:id, :string, primary_key: true)
    field(:external_id, :string, source: :externalId)
    field(:name, :string)
    field(:secret, :string)
    field(:source, :string)
    field(:actions, :string)
    field(:consecutive_failures, :integer, default: 0, source: :consecutiveFailures)
    field(:last_failed_at, Papermark.PrismaDateTime, source: :lastFailedAt)
    field(:disabled_at, Papermark.PrismaDateTime, source: :disabledAt)
    field(:team_id, :string, source: :teamId)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end