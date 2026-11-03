defmodule Typebot.Log do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          status: String.t(),
          context: String.t() | nil,
          description: String.t(),
          details: String.t() | nil,
          inserted_at: DateTime.t(),
          result: Typebot.Result.t() | nil
        }

  schema "Log" do
    field(:id, :string, primary_key: true)
    field(:status, :string)
    field(:context, :string)
    field(:description, :string)
    field(:details, :string)
    field(:result_id, :string, source: :resultId)
    belongs_to(:result, Typebot.Result, foreign_key: :result_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end