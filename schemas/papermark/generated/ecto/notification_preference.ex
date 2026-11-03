defmodule Papermark.NotificationPreference do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          frequency: String.t(),
          scope: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Papermark.User.t() | nil,
          team: Papermark.Team.t() | nil
        }

  schema "NotificationPreference" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:frequency, :string, default: "IMMEDIATE")
    field(:scope, :string, default: "ALL")
    field(:user_id, :string, source: :userId)
    field(:team_id, :string, source: :teamId)
    belongs_to(:user, Papermark.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end