defmodule Papermark.Invitation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          email: String.t(),
          expires: DateTime.t(),
          token: String.t(),
          role: atom(),
          dataroom_ids: [String.t()],
          inserted_at: DateTime.t(),
          team: Papermark.Team.t() | nil
        }

  schema "Invitation" do
    field(:email, :string)
    field(:expires, Papermark.PrismaDateTime)
    field(:token, :string, primary_key: true)
    field(:role, Ecto.Enum, values: [:ADMIN, :MANAGER, :MEMBER, :DATAROOM_MEMBER], default: :MEMBER)
    field(:dataroom_ids, {:array, :string}, source: :dataroomIds)
    field(:team_id, :string, source: :teamId)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end