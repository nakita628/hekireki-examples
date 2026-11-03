defmodule Papermark.UserDataroom do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: String.t(),
          dataroom_id: String.t(),
          inserted_at: DateTime.t(),
          user: Papermark.User.t() | nil,
          team: Papermark.Team.t() | nil,
          dataroom: Papermark.Dataroom.t() | nil
        }

  schema "UserDataroom" do
    field(:user_id, :string, primary_key: true, source: :userId)
    field(:team_id, :string, source: :teamId)
    field(:dataroom_id, :string, primary_key: true, source: :dataroomId)
    belongs_to(:user, Papermark.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end