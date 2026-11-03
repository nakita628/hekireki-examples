defmodule Papermark.Webhook do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          p_id: String.t(),
          name: String.t(),
          url: String.t(),
          secret: String.t(),
          triggers: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Papermark.Team.t() | nil
        }

  schema "Webhook" do
    field(:id, :string, primary_key: true)
    field(:p_id, :string, source: :pId)
    field(:name, :string)
    field(:url, :string)
    field(:secret, :string)
    field(:triggers, :map)
    field(:team_id, :string, source: :teamId)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end