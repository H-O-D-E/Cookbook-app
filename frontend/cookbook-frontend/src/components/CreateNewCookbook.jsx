function CreateNewCookbook() {
  return (
    <div className="flex justify-center text-center item-center">
      <fieldset className="fieldset bg-surface text-foreground rounded-2xl w-lg p-10  text-lg">
        <div className="mb-5">
          <h1 className="text-4xl font-bold">Create new cookbook</h1>
          <p className="mt-1 text-sm font-normal text-muted">
            Please fill out the form to create a new cookbook
          </p>
        </div>

        <label className="label font-semibold text-foreground">
          Cookbook name
        </label>

        <input
          type="text"
          className="input input-lg w-full bg-surface-secondary border-border text-foreground"
          required
        />

        <label className="label mt-3 font-semibold text-foreground">
          Description
        </label>

        <input
          type="text"
          className="input input-lg w-full bg-surface-secondary border-border text-foreground"
          required
        />

        <button
          className="btn mt-7 w-full border-0 bg-call-to-action text-white font-bold"
          type="submit"
        >
          Save
        </button>
      </fieldset>
    </div>
  );
}

export default CreateNewCookbook;
