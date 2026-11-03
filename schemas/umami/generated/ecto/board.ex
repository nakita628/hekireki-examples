defmodule Umami.Board do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          name: String.t(),
          description: String.t(),
          parameters: map(),
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          user: Umami.User.t() | nil,
          team: Umami.Team.t() | nil
        }

  schema "board" do
    field(:id, :string, primary_key: true, source: :board_id)
    field(:type, :string)
    field(:name, :string)
    field(:description, :string)
    field(:parameters, :map)
    belongs_to(:user, Umami.User, foreign_key: :user_id, type: :string)
    belongs_to(:team, Umami.Team, foreign_key: :team_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end