defmodule Trigger.PromptVersion do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          version: integer(),
          text_content: String.t() | nil,
          chat_content: map() | nil,
          model: String.t() | nil,
          config: map() | nil,
          source: String.t(),
          commit_message: String.t() | nil,
          content_hash: String.t(),
          labels: [String.t()],
          created_by: String.t() | nil,
          inserted_at: DateTime.t(),
          prompt: Trigger.Prompt.t() | nil,
          worker: Trigger.BackgroundWorker.t() | nil
        }

  schema "prompt_versions" do
    field(:id, :string, primary_key: true)
    field(:version, :integer)
    field(:text_content, :string, source: :textContent)
    field(:chat_content, :map, source: :chatContent)
    field(:model, :string)
    field(:config, :map)
    field(:source, :string)
    field(:commit_message, :string, source: :commitMessage)
    field(:content_hash, :string, source: :contentHash)
    field(:labels, {:array, :string})
    field(:created_by, :string, source: :createdBy)
    field(:prompt_id, :string, source: :promptId)
    field(:worker_id, :string, source: :workerId)
    belongs_to(:prompt, Trigger.Prompt, foreign_key: :prompt_id, define_field: false, type: :string)
    belongs_to(:worker, Trigger.BackgroundWorker, foreign_key: :worker_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end