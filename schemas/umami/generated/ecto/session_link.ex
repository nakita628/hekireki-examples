defmodule Umami.SessionLink do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          website_id: String.t(),
          session_id: String.t(),
          distinct_id: String.t(),
          inserted_at: DateTime.t() | nil
        }

  schema "session_link" do
    field(:website_id, :string, primary_key: true)
    field(:session_id, :string, primary_key: true)
    field(:distinct_id, :string, primary_key: true)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end