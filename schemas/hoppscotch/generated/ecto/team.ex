defmodule Hoppscotch.Team do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          collections: [Hoppscotch.TeamCollection.t()],
          environments: [Hoppscotch.TeamEnvironment.t()],
          invitations: [Hoppscotch.TeamInvitation.t()],
          members: [Hoppscotch.TeamMember.t()],
          requests: [Hoppscotch.TeamRequest.t()]
        }

  schema "Team" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    has_many(:collections, Hoppscotch.TeamCollection, foreign_key: :team_id, references: :id)
    has_many(:environments, Hoppscotch.TeamEnvironment, foreign_key: :team_id, references: :id)
    has_many(:invitations, Hoppscotch.TeamInvitation, foreign_key: :team_id, references: :id)
    has_many(:members, Hoppscotch.TeamMember, foreign_key: :team_id, references: :id)
    has_many(:requests, Hoppscotch.TeamRequest, foreign_key: :team_id, references: :id)
  end
end