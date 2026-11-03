defmodule Typebot.ChatSession do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          state: map(),
          is_replying: boolean() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "ChatSession" do
    field(:id, :string, primary_key: true)
    field(:state, :map)
    field(:is_replying, :boolean, source: :isReplying)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end