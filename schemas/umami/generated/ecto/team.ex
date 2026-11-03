defmodule Umami.Team do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          access_code: String.t() | nil,
          logo_url: String.t() | nil,
          two_factor_required: boolean(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          websites: [Umami.Website.t()],
          members: [Umami.TeamUser.t()],
          links: [Umami.Link.t()],
          pixels: [Umami.Pixel.t()],
          boards: [Umami.Board.t()]
        }

  schema "team" do
    field(:id, :string, primary_key: true, source: :team_id)
    field(:name, :string)
    field(:access_code, :string)
    field(:logo_url, :string)
    field(:two_factor_required, :boolean, default: false)
    field(:deleted_at, Umami.PrismaDateTime)
    has_many(:websites, Umami.Website, foreign_key: :team_id)
    has_many(:members, Umami.TeamUser, foreign_key: :team_id)
    has_many(:links, Umami.Link, foreign_key: :team_id)
    has_many(:pixels, Umami.Pixel, foreign_key: :team_id)
    has_many(:boards, Umami.Board, foreign_key: :team_id)
    timestamps(inserted_at_source: :created_at)
  end
end