defmodule Hoppscotch.TeamEnvironment do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          variables: map(),
          team: Hoppscotch.Team.t() | nil
        }

  schema "TeamEnvironment" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:variables, :map)
    field(:team_id, :string, source: :teamID)
    belongs_to(:team, Hoppscotch.Team, foreign_key: :team_id, define_field: false, type: :string)
  end
end