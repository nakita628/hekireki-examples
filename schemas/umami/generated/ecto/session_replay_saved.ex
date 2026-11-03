defmodule Umami.SessionReplaySaved do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          visit_id: String.t(),
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          website: Umami.Website.t() | nil
        }

  schema "session_replay_saved" do
    field(:id, :string, primary_key: true, source: :saved_replay_id)
    field(:name, :string)
    field(:visit_id, :string)
    belongs_to(:website, Umami.Website, foreign_key: :website_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end