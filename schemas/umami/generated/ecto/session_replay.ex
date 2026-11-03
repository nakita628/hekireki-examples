defmodule Umami.SessionReplay do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          session_id: String.t(),
          visit_id: String.t(),
          chunk_index: integer(),
          events: binary(),
          event_count: integer(),
          started_at: DateTime.t(),
          ended_at: DateTime.t(),
          inserted_at: DateTime.t() | nil,
          website: Umami.Website.t() | nil
        }

  schema "session_replay" do
    field(:id, :string, primary_key: true, source: :replay_id)
    field(:session_id, :string)
    field(:visit_id, :string)
    field(:chunk_index, :integer)
    field(:events, :binary)
    field(:event_count, :integer)
    field(:started_at, Umami.PrismaDateTime)
    field(:ended_at, Umami.PrismaDateTime)
    belongs_to(:website, Umami.Website, foreign_key: :website_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end